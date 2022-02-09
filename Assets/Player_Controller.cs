using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float m_speed = 20;
    Rigidbody2D rb;
    Animator m_animator;
    public Transform bulletPrefab;

    public List<int> mySteps = new List<int>();
    public Dictionary<string, int[]> combos = new Dictionary<string, int[]>(){
        {"combo1", new int[]{1, 1, 0}},
        {"combo2", new int[]{1, 0, 2}},
        {"combo3", new int[]{2, 1, 0}},
        {"combo4", new int[]{2, 1, 2}},
        {"combo5", new int[]{0, 2, 0}},
        {"combo6", new int[]{1, 0, 2, 0}},
        {"combo7", new int[]{1, 1, 0, 2}},
        {"combo8", new int[]{2, 1, 1, 0, 3}}
    };
    public Dictionary<string, int> combosCode = new Dictionary<string, int>()
    {
        {"combo1", 10},
        {"combo2", 11},
        {"combo3", 12},
        {"combo4", 13},
        {"combo5", 14},
        {"combo6", 15},
        {"combo7", 16},
        {"combo8", 17}
    };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * m_speed, rb.velocity.y);
        if (inputX > 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else if (inputX < 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);
        else
            m_animator.SetInteger("AnimState", 0);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_animator.SetTrigger("Attack");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            m_animator.SetTrigger("Attack");
        }



    }

    public void attackAnimate(string Atype)
    {
        if (Atype.Equals("Range"))
        {
            Instantiate(bulletPrefab, transform.Find("Slash").position, bulletPrefab.rotation);
        }
    }

    public void doCombo()
    {
        Dictionary<string, int[]> powCombos, avPattern = validCombo();
        List<List<int>> indexs;
        List<int> fCombo;
        string name;
        while (avPattern.Any())
        {
            avPattern = validCombo();
            if (avPattern.Any())
            {
                powCombos = powerfulCombos(avPattern);
                indexs = comboIndexes(powCombos);
                fCombo = getFirstCombo(indexs);
                name = getComboName(fCombo);
                comboElimination(fCombo, combosCode[name]);
            }
        }



        mySteps.Clear();
    }
    Dictionary<string, int[]> validCombi(Dictionary<string, int[]> combos, int length, string mode = "")
    {
        Dictionary<string, int[]> toReturn = new Dictionary<string, int[]>();

        foreach (KeyValuePair<string, int[]> combo in combos)
            if (mode == "pow" ? combo.Value.Length == length : combo.Value.Length <= length)
                toReturn.Add(combo.Key, combo.Value);

        return toReturn;
    }
    Dictionary<string, int[]> validCombo()
    {
        Dictionary<string, int[]> toReturn = new Dictionary<string, int[]>();

        foreach (KeyValuePair<string, int[]> combo in validCombi(combos, mySteps.Count))
        {
            for (int i = 0; i <= boxPattern(mySteps, combo.Value); i++)
            {
                int rightCount = combo.Value.Length;
                for (int j = 0; j < combo.Value.Length; j++)
                    rightCount -= mySteps[i + j] != combo.Value[j] ? 1 : 0;

                if (rightCount == combo.Value.Length)
                    try
                    {
                        toReturn.Add(combo.Key, combo.Value);
                    }
                    catch (System.Exception)
                    {
                    }
            }
        }

        return toReturn;
    }
    int boxPattern(List<int> list, int[] arr2)
    {
        return list.Count - arr2.Length;
    }

    Dictionary<string, int[]> powerfulCombos(Dictionary<string, int[]> combos)
    {
        int max = 0;
        foreach (var combo in combos)
            if (combo.Value.Length > max)
                max = combo.Value.Length;

        return validCombi(combos, max, "pow");
    }

    List<List<int>> comboIndexes(Dictionary<string, int[]> combos)
    {
        List<List<int>> toReturn = new List<List<int>>();
        foreach (var combo in combos)
        {
            for (int i = 0; i <= boxPattern(mySteps, combo.Value); i++)
            {
                int rightCount = combo.Value.Length;
                List<int> indexes = new List<int>();
                for (int j = 0; j < combo.Value.Length; j++)
                {
                    rightCount -= mySteps[i + j] != combo.Value[j] ? 1 : 0;
                    indexes.Add(i + j);
                }
                if (rightCount == combo.Value.Length)
                    toReturn.Add(indexes);
            }
        }

        return toReturn;
    }
    List<int> getFirstCombo(List<List<int>> arr)
    {
        List<int> toReturn = new List<int>();
        int min = arr[0].Sum();
        foreach (var combo in arr)
        {
            if (combo.Sum() <= min)
            {
                min = combo.Sum();
                toReturn = combo;
            }
        }
        return toReturn;
    }
    void comboElimination(List<int> arr, int code)
    {
        mySteps[arr[0]] = code;
        mySteps.RemoveRange(arr[1], arr.Count - 1);
    }

    string getComboName(List<int> elements)
    {
        foreach (var item in validCombi(combos, elements.Count, "pow"))
        {
            var check = item.Value.SequenceEqual(mySteps.GetRange(elements[0], elements.Count));
            if (check)
            {
                return item.Key;
            }
        };
        return "";
    }

    public void addCombo(int step)
    {
        mySteps.Add(step);
    }
}

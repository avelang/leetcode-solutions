using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ListNode
{
    public int val;
    public ListNode(int x) { val = x; }
    public ListNode next;
}
public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        char[] c = s.ToCharArray();
        int len = c.Length;
        int count = 0;
        int MaxCount = 0;
        Hashtable ht = new Hashtable();

        for (int i = 0; i < len; i++)
        {
            if (ht.ContainsKey(c[i]))
            {
                if (count > MaxCount)
                {
                    MaxCount = count;
                }
                i = (int)ht[c[i]]+1;
                ht.Clear();
                ht.Add(c[i], i);
                count = 1;
            }
            else
            {
                ht.Add(c[i], i);
                count++; //
                if (count > MaxCount)
                {
                    MaxCount = count;
                }
            }
        }
        return MaxCount;
    }
    public int RemoveDuplicates(int[] nums)
    {
        if(nums.Length == 0)
        {
            return 0;
        }
        int distinctCount = 1;
        int i = 0;
        for (int j = 1; j < nums.Length; j++)
        {
            if(nums[i]!=nums[j])
            {
                nums[i + 1] = nums[j];
                i++;
                distinctCount++;
            }
        }
        return distinctCount;
    }
    // 14/04/2020
    public int[] CreateTargetArray(int[] nums, int[] index)
    {
        int totIndices = 0;
        int[] result = new int[index.Length];
        Hashtable addedIndices = new Hashtable();
        for (int i = 0; i < index.Length; i++)
        {
            // if addedIndices.Contains(index[i]), shift elements by 1
            // from totIndices to index[i]
            if (addedIndices.Contains(index[i]) || result[index[i]] > 0)
            {
                for (int j = totIndices-1; j >= index[i]; j--)
                {
                    result[j + 1] = result[j];
                }
            }
            result[index[i]] = nums[i];
            if(!addedIndices.ContainsKey(index[i]))
            {
                addedIndices.Add(index[i], 1);
            }
            totIndices++;
        }
        return result;
    }
    // use dictionary instead of hashtable to avoid boxing/unboxing issues
    public int SingleNumber(int[] nums)
    {
        Dictionary<int,int> ht = new Dictionary<int, int>();

        IList singleNum = new List<int>() { };
        for (int i = 0; i < nums.Length; i++)
        {
            if(ht.ContainsKey(nums[i]))
            {
                ht.Remove(nums[i]);
            }
            else
            {
                ht.Add(nums[i],1);
            }
        }
        foreach(var item in ht)
        {
            singleNum.Add(Convert.ToInt32(item.Key));
        }
        return Convert.ToInt32(singleNum[0]);
    }
    // Warm-Up Exercise : Check if the given string is an anagram of the other
    // note - converting string to char array & back
    public bool IsAnagram(string s, string t)
    {
        char[] sorted_s = s.ToCharArray();
        char[] sorted_t = t.ToCharArray();
        Array.Sort<char>(sorted_s);
        Array.Sort<char>(sorted_t);
        return String.Compare(new string(sorted_s), new string(sorted_t)) == 0;
    }
    public bool IsPalindrome(ListNode head)
    {
        ListNode current = head;
        List<int> nums = new List<int>{ };
        int count = 0;
        bool isPalindrome = true;
        while (current != null)
        {
            nums.Add(current.val);
            count++;
            current = current.next;
        }
        int midIndex = (nums.Count % 2 == 0) ? (nums.Count / 2) : ((nums.Count - 1) / 2);
        for (int i = 0; i < midIndex; i++)
        {
            if (nums[i] != nums[nums.Count - 1 - i])
            {
                isPalindrome = false;
                break;
            }
        }
        return isPalindrome;
    }
    /*
     Given a string containing only three types of characters: '(', ')' and '*', write a function to check whether this string is valid. 
     We define the validity of a string by these rules:
     Any left parenthesis '(' must have a corresponding right parenthesis ')'.
     Any right parenthesis ')' must have a corresponding left parenthesis '('.
     Left parenthesis '(' must go before the corresponding right parenthesis ')'.
     '*' could be treated as a single right parenthesis ')' or a single left parenthesis '(' or an empty string.
     An empty string is also valid.   
     */
    public bool CheckValidString(string s)
    {
        char openChar = '(';
        char closeChar = ')';
        char wildcardChar = '*';
        bool isValid = false;

        string newStr = NormalizeString(s, '(', ')'); // new string(array).Replace(" ", string.Empty);

        if(newStr.Length>0)
        {
            if (newStr.Contains("(") && !newStr.Contains(")"))
            {
                string modString = NormalizeString(newStr, '(', '*');
                if (!modString.Contains('('))
                {
                    isValid = true;
                }
            }
            else if (!newStr.Contains("(") && newStr.Contains(")"))
            {
                string modString = NormalizeString(newStr, '*', ')');
                if (!modString.Contains(')'))
                {
                    isValid = true;
                }
            }
            else if (newStr.Contains("(") && newStr.Contains(")"))
            {
                // the reduced string has both open & closed chars, so split into two strings & normalize
                string newCloseStr = newStr.Substring(0, newStr.LastIndexOf(closeChar) + 1); // eg : "***))"
                string newOpenStr = newStr.Substring(newStr.IndexOf(openChar)); // eg: "*((***"
                if (!NormalizeString(newCloseStr, wildcardChar, closeChar).Contains(closeChar) &&
                    !NormalizeString(newOpenStr, openChar, wildcardChar).Contains(openChar))
                    isValid = true;
            }
            else if (!newStr.Contains("(") && !newStr.Contains(")") && newStr.Contains("*"))
            {
                isValid = true;
            }
        }
        else
        {
            isValid = true;
        }
        return isValid;
    }
    private static string NormalizeString(string str, char openChar, char closeChar)
    {
        Stack<int> OpenIndex = new Stack<int>();
        char[] newCharArray = str.ToCharArray();
        for (int i = 0; i < newCharArray.Length; i++)
        {
            if (newCharArray[i] == openChar)
            {
                OpenIndex.Push(i);
            }
            else if (newCharArray[i] == closeChar)
            {
                if (OpenIndex.Count > 0)
                {
                    int indexNum = OpenIndex.Pop();
                    newCharArray[indexNum] = ' ';
                    newCharArray[i] = ' ';
                }
            }
        }
        return new string(newCharArray).Replace(" ", string.Empty);
    }
    public void MoveZeroes(int[] nums)
    {
        int zeroCount = 0;
        List<int> zeroIndices = new List<int>();

        for (int i = 0; i < nums.Length - zeroCount; i++)
        {
            if (nums[i] == 0)
            {
                zeroCount++;
                Bubble(nums, i, nums.Length - zeroCount);
                i--;
            }
        }
    }
    public void Bubble(int[] nums, int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            nums[i] = nums[i + 1];
        }
        nums[end] = 0;
    }
    // maximise profit by doing buy & sell stocks
    public int MaxProfit(int[] prices)
    {
        int[] bottoms = { };
        int[] tops = { };
        int minIndex = -1;
        int maxIndex = -1;
        int profit = 0;

        for (int i = 0; i < prices.Length; i++)
        {
            // identify min index
            if (i < prices.Length - 1)
            {
                if (i == 0)
                {
                    if (prices[i + 1] > prices[i])
                        minIndex = i;
                }
                else
                {
                    if ((prices[i - 1] >= prices[i] && prices[i] < prices[i + 1])||
                        (prices[i - 1] > prices[i] && prices[i] <= prices[i + 1]))
                        minIndex = i;
                }
            }
            // identify max index
            if (i > 0)
            {
                if (i == prices.Length - 1)
                {
                    if (prices[i - 1] < prices[i])
                    {
                        maxIndex = i;
                    }
                }
                else
                {
                    if ((prices[i - 1] <= prices[i] && prices[i] > prices[i + 1])||
                        (prices[i - 1] < prices[i] && prices[i] >= prices[i + 1]))
                        if (minIndex!=-1)
                        {
                            maxIndex = i;
                        }
                }
            }
            if (minIndex != -1 && maxIndex != -1 && (maxIndex> minIndex))
            {
                profit = profit + prices[maxIndex] - prices[minIndex];
                minIndex = -1;
                maxIndex = -1;
            }
        }
        return profit;
    }
}


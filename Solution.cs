using System;
using System.Collections;
using System.Collections.Generic;

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

    public bool CheckValidString(string s)
    {
        // Re-do
        // if openCount==0 && starCount==0 && s[i]==')' -> return false
        // if openCount==0 && starCount>0 && s[i]==')'-> openCount--;
        // in the end , if starCount >= opencount, return true;
        int openCount = 0;
        int closeCount = 0;
        int starCount = 0;
        Stack<int> lastOpenIndex = new Stack<int>();
        char[] array = s.ToCharArray();

        for (int i = 0; i < array.Length; i++)
        {
            if (s[i] == '(')
            {
                // openCount++;
                lastOpenIndex.Push(i);
            }
            else if (s[i] == '*')
            {
                starCount++;
            }
            else if (s[i] == ')')
            {
                //if ((openCount == 0 && closeCount == 0))
                //{
                //    return false;
                //}
                //else
                //{
                //    closeCount++;
                //}
                if (lastOpenIndex.Count > 0)
                {
                    int indexNum = lastOpenIndex.Pop();
                    array[indexNum] = ' ';
                    array[i] = ' ';
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            //if ((openCount + starCount < closeCount))
            //{
            //    return false;
            //}
        }
        string newStr = new string(array).Replace(" ",string.Empty);
        // this new string can have *s plus either '(' or ')'
        // if string contains '(', get count of that, see if all the open chars has a corresponding *
        // i.e. string starting at the first index of '(' till the end, the count(*s) >= count('(')
        // Similarly, for ')'
        // ....
        if (((Math.Abs(openCount - closeCount) > 0) && (Math.Abs(openCount - closeCount) <= starCount)) || (openCount - closeCount == 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
public class ListNode
{
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
 }

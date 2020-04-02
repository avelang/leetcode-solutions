using System.Collections;

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
}

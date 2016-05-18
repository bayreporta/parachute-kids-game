using UnityEngine;
using System.Collections;

public class ProcessMath : MonoBehaviour {

    public static int Add_Nums(params int[] nums) {
        int tot = 0;
        foreach (int num in nums) {
            tot += num;
        }
        return tot;
    }
}

package other;

class Problem27 {

    int removeElement(int[] nums, int val) {
        int delta = 0;
        for (int i = 0; i < nums.length - delta; i++) {
            if (nums[i] == val) {
                if (nums.length - i - 1 >= 0) {
                    System.arraycopy(nums, i + 1, nums, i, nums.length - i - 1);
                }
                delta++;
                i--;
            }
        }
        return nums.length - delta;
    }
}

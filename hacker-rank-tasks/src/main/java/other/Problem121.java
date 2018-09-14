package other;

class Problem121 {

    int maxProfit(int[] prices) {
        int maxProfit = 0;
        for (int buy = 0; buy < prices.length; buy++) {
            for (int sell = buy + 1; sell < prices.length; sell++) {
                final int profit = prices[sell] - prices[buy];
                if (profit > maxProfit) {
                    maxProfit = profit;
                }
            }
        }
        return maxProfit;
    }
}

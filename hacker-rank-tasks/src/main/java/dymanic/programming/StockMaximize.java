package dymanic.programming;

/**
 * Created by Sergey on 28.09.2017.
 */
public class StockMaximize {

    private static class Situation {
        int money;
        int stocks;

        Situation(final int money, final int stocks) {
            this.money = money;
            this.stocks = stocks;
        }
    }

    private static int[] prices = {1, 3, 1, 2};

    public static void main(String[] args) {
        final Situation situation = optimalSituationForDay(3);
        System.out.println(situation.money);
    }

    private static Situation optimalSituationForDay(int today) {
        if (today == -1) {
            return new Situation(0, 0);
        }
        Situation optimalYesterdaySituation = optimalSituationForDay(today - 1);
        return situationWithMaximumMoney(optimalYesterdaySituation, new Situation(optimalYesterdaySituation.money - prices[today], optimalYesterdaySituation.stocks + 1), new Situation(optimalYesterdaySituation.money + optimalYesterdaySituation.stocks * prices[today], 0));
    }

    private static Situation situationWithMaximumMoney(final Situation first, final Situation second, final Situation third) {
        if (first.money > second.money) {
            return first.money > third.money ? first : third;
        } else {
            return second.money > third.money ? second : third;
        }
    }
}

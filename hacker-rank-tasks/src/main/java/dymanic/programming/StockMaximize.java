package dymanic.programming;

/**
 * Created by Sergey on 28.09.2017.
 */
public class StockMaximize {

    private static class Situation {
        int day;
        int money;
        int stocks;

        public Situation(final int day, final int money, final int stocks) {
            this.day = day;
            this.money = money;
            this.stocks = stocks;
        }

        public Situation(final int money, final int stocks) {
            this.money = money;
            this.stocks = stocks;
        }
    }

    int[] prices = new int[0];

    public static void main(String[] args) {

    }

    public void situation(int day, int money, int stocks) {

    }

    Situation optimalSituationForDay(int today) {
        Situation optimalYesterdaySituation = optimalSituationForDay(today - 1);
        return situationWithMaximumMoney(optimalYesterdaySituation, new Situation(optimalYesterdaySituation.money - prices[today], optimalYesterdaySituation.stocks + 1), new Situation(optimalYesterdaySituation.money + optimalYesterdaySituation.stocks * prices[today], 0));
    }

    Situation bestSituationAfter(final Situation situation) {
        final int today = situation.day;
        final int yesterday = today - 1;
        final int money = situation.money;
        final int stocks = situation.stocks;
        return situationWithMaximumMoney(bestSituationAfter(new Situation(yesterday, money, stocks)),
                bestSituationAfter(new Situation(yesterday, money - prices[today], stocks + 1)), bestSituationAfter(new Situation(yesterday, money + stocks * prices[today], 0)));
    }

    Situation situationWithMaximumMoney(final Situation first, final Situation second, final Situation third) {
        if (first.money > second.money) {
            return first.money > third.money ? first : third;
        } else {
            return second.money > third.money ? second : third;
        }
    }
}

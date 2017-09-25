import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by Sergey on 03.09.2017.
 */
public class TimeConversion {

    private static String timeConversion(String time) {
        // Complete this function
        final Pattern pattern = Pattern.compile("(\\d\\d):(\\d\\d:\\d\\d)(AM|PM)");
        final Matcher matcher = pattern.matcher(time);
        if (matcher.matches()) {
            final String hours = matcher.group(1);
            final String secondsAndMilliseconds = matcher.group(2);
            final String amOrPm = matcher.group(3);
            final int numberOfHours = Integer.parseInt(hours);
            if ("AM".equals(amOrPm)) {
                return numberOfHours == 12 ? "00:" + secondsAndMilliseconds : hours + ":" + secondsAndMilliseconds;
            } else if ("PM".equals(amOrPm)) {
                return numberOfHours == 12 ? "12:" + secondsAndMilliseconds : (numberOfHours + 12) + ":" + secondsAndMilliseconds;
            } else {
                throw new RuntimeException("should be AM or PM, but was" + amOrPm);
            }
        } else {
            throw new RuntimeException("");
        }
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        String s = in.next();
        String result = timeConversion(s);
        System.out.println(result);
    }
}

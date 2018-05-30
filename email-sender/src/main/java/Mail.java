import com.google.common.io.Resources;

import javax.mail.Authenticator;
import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import java.io.IOException;
import java.nio.charset.Charset;
import java.util.Properties;
import java.util.Scanner;

import static com.google.common.io.Resources.getResource;
import static java.util.Arrays.stream;
import static javax.mail.Message.RecipientType.TO;

public class Mail {

    public static void main(String[] args) throws IOException {
        final String username = "yaskovdev@gmail.com";
        System.out.println("Enter password for " + username + " and press Enter:");
        final String password = new Scanner(System.in).next();

        final Properties props = new Properties();
        props.put("mail.smtp.auth", "true");
        props.put("mail.smtp.starttls.enable", "true");
        props.put("mail.smtp.host", "smtp.gmail.com");
        props.put("mail.smtp.port", "587");

        Session session = Session.getInstance(props,
                new Authenticator() {
                    protected PasswordAuthentication getPasswordAuthentication() {
                        return new PasswordAuthentication(username, password);
                    }
                });

        stream(emails()).forEach(email -> sendMessage(session, email));
    }

    private static void sendMessage(final Session session, final String email) {
        try {
            final Message message = new MimeMessage(session);
            message.setFrom(new InternetAddress("yaskovdev@gmail.com", "Sergey Yaskov"));
            message.setRecipients(TO, InternetAddress.parse(email));
            message.setSubject("New Article");
            message.setText(text());

            Transport.send(message);

            System.out.println("Sent to " + email);
        } catch (final MessagingException | IOException e) {
            throw new RuntimeException(e);
        }
    }

    private static String[] emails() throws IOException {
        final String string = Resources.toString(getResource("emails.txt"), Charset.forName("UTF-8"));
        return string.split(",\\s*");
    }

    private static String text() throws IOException {
        return Resources.toString(getResource("message.txt"), Charset.forName("UTF-8"));
    }
}

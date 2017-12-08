import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

/**
 * https://stackoverflow.com/questions/47345057/how-to-read-multiple-text-files-from-a-folder-and-append-it-to-a-new-file-in-jav
 */
public class Question47345057 {

    public static void main(final String[] args) throws IOException {
        final File directory = new File("C:/Users/sr01407/Desktop/test");
        final File[] files = directory.listFiles((dir, name) -> name.endsWith(".txt"));
        if (files == null) {
            throw new IOException("the file " + directory + " is not a directory");
        }
        try (final BufferedWriter output = new BufferedWriter(new FileWriter("C:/Users/sr01407/Desktop/numbers.txt", true))) {
            for (final File file : files) {
                try (final BufferedReader reader = new BufferedReader(new FileReader(file))) {
                    String line;
                    while ((line = reader.readLine()) != null) {
                        output.write(line);
                        output.newLine();
                    }
                }
            }
        }
    }
}

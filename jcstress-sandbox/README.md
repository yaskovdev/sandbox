Built from the archetype as described [here](https://github.com/openjdk/jcstress?tab=readme-ov-file#using-jcstress-as-separate-dependency).

Following along with the https://shipilev.net/blog/2016/close-encounters-of-jmm-kind/ tutorial.

```shell
mvn clean verify

java -jar target/jcstress.jar
# or
java -jar target/jcstress.jar -t com.yaskovdev.Question79288845Test
```

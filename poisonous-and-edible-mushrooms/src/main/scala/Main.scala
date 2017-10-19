import scala.io.Source
import scala.math.log10

object Main {

  def main(args: Array[String]): Unit = {
    val iterator: Iterator[Array[String]] = Source.fromResource("agaricus-lepiota.data").getLines().map(_.split(","))
    val mushrooms = iterator.map(l => Mushroom(l(0) == "e", l(1), l(2), l(3), l(4) == "t", l(5), l(6), l(7),
      l(8), l(9), l(10), l(11), l(12), l(13), l(14), l(15), l(16), l(17), l(18), l(19), l(20), l(21), l(22))).toSeq
    val edibleMushrooms = mushrooms.filter(_.edible)
    val poisonousMushrooms = mushrooms.filter(!_.edible)
    val edibleFraction = edibleMushrooms.size.toDouble / mushrooms.size
    println(edibleFraction)
    val poisonousFraction = poisonousMushrooms.size.toDouble / mushrooms.size
    println(poisonousFraction)
    val withRedCap = mushrooms.filter(_.capColor == "e")
    val withRedCapEdible = withRedCap.filter(_.edible)
    println(withRedCapEdible.size.toDouble / withRedCap.size)
    println(entropy(mushrooms))
    println(entropy(mushrooms.groupBy(_.capShape)))
    println(entropy(mushrooms.groupBy(_.capSurface)))
    println(entropy(mushrooms.groupBy(_.capColor)))
  }

  def entropy(partitioning: Map[_, Seq[Mushroom]]): Double = {
    val values: Iterable[Double] = partitioning.values.map(entropy)
    values.sum
  }

  def entropy(set: Seq[Mushroom]): Double = {
    val (edible, _) = set.partition(_.edible)
    val p = edible.size.toDouble / set.size
    if (p == 0 || 1 - p == 0) 0 else -p * log2(p) - (1 - p) * log2(1 - p)
  }

  def log2(x: Double): Double = log10(x) / log10(2.0)
}

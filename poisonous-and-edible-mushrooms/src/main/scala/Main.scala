import scala.io.Source
import scala.math.log10

object Main {

  def main(args: Array[String]): Unit = {
    val iterator: Iterator[Array[String]] = Source.fromResource("agaricus-lepiota.data").getLines().map(_.split(","))
    val mushrooms = iterator.map(l => Mushroom(l(0) == "e", l(1), l(2), l(3), l(4) == "t", l(5), l(6), l(7),
      l(8), l(9), l(10), l(11), l(12), l(13), l(14), l(15), l(16), l(17), l(18), l(19), l(20), l(21), l(22))).toSeq
    println(informationGain(mushrooms, mushrooms.groupBy(_.capShape).values) + " cap shape")
    println(informationGain(mushrooms, mushrooms.groupBy(_.capSurface).values) + " cap surface")
    println(informationGain(mushrooms, mushrooms.groupBy(_.capColor).values) + " cap color")
    println(informationGain(mushrooms, mushrooms.groupBy(_.bruises).values) + " bruises")
    println(informationGain(mushrooms, mushrooms.groupBy(_.odor).values) + " odor")
    println(informationGain(mushrooms, mushrooms.groupBy(_.gillAttachment).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.gillSpacing).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.gillSize).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.gillColor).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.stalkShape).values) + " stalk shape")
    println(informationGain(mushrooms, mushrooms.groupBy(_.stalkRoot).values) + " stalk root")
    println(informationGain(mushrooms, mushrooms.groupBy(_.stalkSurfaceAboveRing).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.stalkSurfaceBelowRing).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.stalkColorAboveRing).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.stalkColorBelowRing).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.veilType).values) + " veil type")
    println(informationGain(mushrooms, mushrooms.groupBy(_.veilColor).values) + " veil color")
    println(informationGain(mushrooms, mushrooms.groupBy(_.ringNumber).values) + " ring number")
    println(informationGain(mushrooms, mushrooms.groupBy(_.ringType).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.sporePrintColor).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.population).values))
    println(informationGain(mushrooms, mushrooms.groupBy(_.habitat).values))
  }

  def informationGain(parent: Seq[Mushroom], children: Iterable[Seq[Mushroom]]): Double = {
    val values: Iterable[(Int, Double)] = children.map(child => (child.size, entropy(child)))
    entropy(parent) - values.map(value => (value._1.toDouble / parent.size) * value._2).sum
  }

  def entropy(set: Seq[Mushroom]): Double = {
    val (edible, _) = set.partition(_.edible)
    val p = edible.size.toDouble / set.size
    if (p == 0 || 1 - p == 0) 0 else -p * log2(p) - (1 - p) * log2(1 - p)
  }

  def log2(x: Double): Double = log10(x) / log10(2.0)
}

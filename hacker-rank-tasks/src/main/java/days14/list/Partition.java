package days14.list;

/**
 * 2.4 (page 94)
 */
public class Partition {

    Node partition(final Node list, final int x) {
        Node lts = null;
        Node lte = null;
        Node gtets = null;
        Node gtete = null;
        Node p = list;
        while (p != null) {
            if (p.data < x) {
                if (lts == null) {
                    lts = p;
                    lte = p;
                } else {
                    lte.next = p;
                    lte = p;
                }
            } else {
                if (gtets == null) {
                    gtets = p;
                    gtete = p;
                } else {
                    gtete.next = p;
                    gtete = p;
                }
            }
            p = p.next;
        }
        gtete.next = null;
        lte.next = gtets;
        return lts;
    }
}

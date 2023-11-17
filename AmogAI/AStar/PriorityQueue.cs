namespace AmogAI.AStar;

public class PriorityQueue<T> {
    //public static readonly int DEFAULT_CAPACITY = 100;
    //public int size;   // elements in heap
    //public (int, T)[] array;  // on heap

    //public PriorityQueue() {
    //    array = new (int, T)[DEFAULT_CAPACITY + 1];
    //    size = 0;
    //}

    //public void Add((int, T) item) {
    //    if (size == array.Length - 1)
    //        DoubleArray();

    //    int hole = ++size;
    //    array[0] = item;

    //    // swap if new item is smaller than parent -> Perculate up
    //    for (; item.Item1.CompareTo(array[hole / 2].Item1) < 0; hole /= 2)
    //        array[hole] = array[hole / 2];

    //    array[hole] = item;
    //}

    //// removes the smallest item in the priority queue
    //public int Remove() {
    //    if (size == 0)
    //        throw new Exception();

    //    (int, T) hole = (1, null);
    //    (int, T) min = array[hole.Item1].Item1;
    //    array[hole] = array[size];

    //    PercolateDown(hole);
    //    size--;
    //    return min;
    //}

    //public void Clear() {
    //    array = new (int, T)[DEFAULT_CAPACITY + 1];
    //    size = 0;
    //}

    //private void PercolateDown((int, T) hole) {
    //    (int, T) child;
    //    (int, T) temp = array[hole.Item1];

    //    for (; hole.Item1 * 2 <= size; hole = child) {
    //        child = hole.Item1 * 2;
    //        if (child.Item1 != size && array[child.Item1 + 1].CompareTo(array[child.Item1]) < 0)
    //            child.Item1++;

    //        if (array[child.Item1].CompareTo(temp) < 0)
    //            array[hole.Item1] = array[child.Item1];
    //        else
    //            break;
    //    }

    //    array[hole.Item1] = temp;
    //}

    //private void DoubleArray() {
    //    (int, T) temp = new (int, T)[array.Length * 2];
    //    array.CopyTo(temp, 0);
    //    array = temp;
    //}

    //public override string ToString() {
    //    if (size == 0)
    //        return "";

    //    var returnValue = new StringBuilder();
    //    for (int i = 1; i <= size; i++)
    //        returnValue.Append(array[i] + (i != size? " " : ""));

    //    return returnValue.ToString();
    //}

    //public void AddFreely((int, T) item) {
    //    if (size == array.Length - 1)
    //        DoubleArray();

    //    size++;
    //    array[size] = item;
    //}

    //public void BuildHeap() {
    //    // start at the highest number nonleaf -> percolate down (does not have to be used on a leaf)
    //    for (int i = size / 2; i > 0; i--)
    //        PercolateDown(i);
    //}


}

public class PriorityQueueEmptyException : System.Exception {
    // Is thrown when Remove is called on an empty queue
}
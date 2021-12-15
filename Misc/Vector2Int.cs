namespace AOC2021 {
    public class Vector2Int {
        public int X;
        public int Y;

        public Vector2Int() { }

        public Vector2Int(int x, int y) {
            X = x; 
            Y = y;
        }

        public override bool Equals(object obj) {
            Vector2Int v2i = (Vector2Int)obj;
            if (v2i == null) { return false; }
            return X == v2i.X && Y == v2i.Y;
        }

        public override int GetHashCode() {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}

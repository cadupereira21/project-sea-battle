using JetBrains.Annotations;

namespace Exception {
    public class ObjectOverlappingException : System.Exception {
        public ObjectOverlappingException([CanBeNull] string message = null) : base(message ?? "Object is overlapping with another object") {
        }
    }
}
using JetBrains.Annotations;
using UnityEngine;

namespace Exception {
    public abstract class UserException : System.Exception {

        public string ErrorTitle { get; private set; }

        public string ErrorMessage { get; private set; }
        
        protected UserException(string errorTitle, string message) : base(message) {
            ErrorMessage = message;
            ErrorTitle = errorTitle;
        }
    }
}
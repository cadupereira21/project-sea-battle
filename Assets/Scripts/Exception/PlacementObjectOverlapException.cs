namespace Exception {
    public class PlacementObjectOverlapException : UserException {
        
        private const string DEFAULT_ERROR_TITLE = "Colisão detectada!";
        
        private const string DEFAULT_ERROR_MESSAGE = "Abortando o movimento do seu navio";
        
        public PlacementObjectOverlapException() 
            : base(DEFAULT_ERROR_TITLE, DEFAULT_ERROR_MESSAGE) { }
    }
}
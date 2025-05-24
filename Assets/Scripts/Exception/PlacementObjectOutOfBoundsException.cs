namespace Exception {
    public class PlacementObjectOutOfBoundsException : UserException {

        private const string DEFAULT_ERROR_TITLE = "Fora dos limites!";
        
        private const string DEFAULT_ERROR_MESSAGE = "Seu navio estava navegando para fora das águas do combate";
        
        public PlacementObjectOutOfBoundsException() 
            : base(DEFAULT_ERROR_TITLE, DEFAULT_ERROR_MESSAGE) { }

    }
}
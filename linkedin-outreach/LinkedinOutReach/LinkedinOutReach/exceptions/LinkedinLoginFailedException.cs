using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedinOutReach.exceptions
{
    public class LinkedinLoginFailedException : Exception
    {
        // Constructeur par défaut
        public LinkedinLoginFailedException() { }

        // Constructeur avec un message personnalisé
        public LinkedinLoginFailedException(string message) : base(message) { }

        // Constructeur avec message et exception interne (pour garder la trace de l'erreur originale)
        public LinkedinLoginFailedException(string message, Exception inner) : base(message, inner) { }
    }
}

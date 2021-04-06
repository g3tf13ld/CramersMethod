using System;

namespace CramersSolution.Models
{
    public class CramersSolutionNotFoundException : Exception {
        public CramersSolutionNotFoundException(string msg)
            : base("Solution couldn't be found: \r\n" + msg) {
        }

        public CramersSolutionNotFoundException() : base()
        {
        }

        public CramersSolutionNotFoundException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
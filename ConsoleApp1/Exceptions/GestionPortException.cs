// <copyright file="GestionPortException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GestionNavire.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Classe qui gère toutes les Exceptions.
    /// </summary>
    public class GestionPortException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GestionPortException"/> class.
        /// Déclencheur d'exception.
        /// </summary>
        /// <param name="message">string de l'exception.</param>
        public GestionPortException(string message)
            : base("Erreur de :" + System.Environment.UserName + " le " + DateTime.Now.ToLocalTime() + "\n" + message)
        {
        }
    }
}

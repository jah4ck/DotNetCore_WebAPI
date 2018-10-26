using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetCore_WebAPI.entities
{
    /// <summary>
    /// permet de stocker les résultat des méthodes
    /// </summary>
    [DataContract]
    public class TaskResult<T>
    {
        #region boolean
        /// <summary>
        /// Permet au classe métier de vérifier les règle de gestion et de dire si oui ou non on est autorisé a effectuer l'action
        /// le but est d'éviter le spam de requête
        /// </summary>
        [DataMember(Name = "Authorize")]
        public bool Authorize { get; set; } = true; //true par défault

        /// <summary>
        /// on stock ici si l'action a été réussite avec succès
        /// </summary>
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }
        #endregion


        #region erreur warning info
        /// <summary>
        /// stockage de l'exception en cas d'erreur
        /// </summary>
        [DataMember(Name = "Exception")]
        public Exception Exception { get; set; }

        /// <summary>
        /// stockage d'un message string lié à l'action
        /// </summary>
        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }
        #endregion


        #region stockage des data
        /// <summary>
        /// permet le stockage de plusieurs retour sql 
        /// </summary>
        [DataMember(Name = "LstResultSet")]
        public ICollection<dynamic> LstResultSet { get; set; }

        /// <summary>
        /// stock une énumération générique ( 1 result set sql)
        /// </summary>
        [DataMember(Name = "LstResult")]
        public IEnumerable<T> LstResult { get; set; }

        /// <summary>
        /// stock un objet de façon dynamique
        /// </summary>
        [DataMember(Name = "Result")]
        public T Result { get; set; }
        #endregion


    }
}

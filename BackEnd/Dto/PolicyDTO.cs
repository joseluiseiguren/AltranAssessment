namespace BackEnd.Dto
{
    using System;

    /// <summary>
    /// DTO de policies
    /// </summary>
    public class PolicyDTO
    {
        /// <summary>
        /// id de la policy
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// monto asegurado de la policy
        /// </summary>
        public decimal AmountInsured { get; set; }

        /// <summary>
        /// email del dueño de la policy
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// fecha de inicio
        /// </summary>
        public DateTime InceptionDate { get; set; }

        /// <summary>
        /// ultimo pago
        /// </summary>
        public bool InstallmentPayment { get; set; }

        /// <summary>
        /// id del cliente dueño de la poliza
        /// </summary>
        public string ClientId { get; set; }
    }
}
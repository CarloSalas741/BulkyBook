using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public static class SD
    {
        public const string ROLE_USER_IND = "Individual";
        public const string ROLE_USER_COMP = "Company";
        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_EMPLOYEE = "Employee";

        public const string STATUS_PENDING = "Pending";
        public const string STATUS_APPROVED = "Approved";
        public const string STATUS_IN_PROCESS = "Processing";
        public const string STATUS_SHIPPED = "Shipped";
        public const string STATUS_CANCELLED = "Cancelled";
        public const string STATUS_REFUNDED = "Refunded";

        public const string PAYMENTSTATUS_PENDING = "Pending";
        public const string PAYMENTSTATUS_APPROVED = "Approved";
        public const string PAYMENTSTATUS_DELAYEDPAYMENT = "ApprovedForDelayedPayment";
        public const string PAYMENTSTATUS_REJECTED = "Rejected";

        public const string SESSIONCART = "SessionShoppingCart";

    }
}

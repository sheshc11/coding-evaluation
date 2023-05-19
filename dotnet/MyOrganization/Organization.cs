using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        private int id;

        public Organization()
        {
            root = CreateOrganization();
            id = 0;
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            return HireEmployeeRecursive(root, person, title);
        }

        /**
         * Recursive method to hire the given person as an employee in the position that has that title
         * 
         * @param position
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? HireEmployeeRecursive(Position position, Name person, string title)
        {
            if (position.GetTitle().Equals(title))
            {
                id++;
                position.SetEmployee(new Employee(id, person));
                return position;

            } else {
                foreach (Position p in position.GetDirectReports())
                {
                    HireEmployeeRecursive(p, person, title);
                }
            }

            return null;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProdData.Models
{
    public class ProgramModel
    {
        private string _programName;
        public string ProgramName
        {
            get
            {
                return _programName;
            }
            set
            {
                _programName = value;
            }
        }

        private string _productRelationship;
        public string ProductRelationship
        {
            get
            {
                return _productRelationship;
            }
            set
            {
                _productRelationship = value;
            }
        }

        private string _programCreator;
        public string ProgramCreator
        {
            get
            {
                return _programCreator;
            }
            set
            {
                _programCreator = value;
            }
        }

        public ProgramModel(string programName, string productRelationship, string programCreator)
        {
            _programName = programName;
            _productRelationship = productRelationship;
            _programCreator = programCreator;
        }
    }
}

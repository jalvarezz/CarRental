using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Extensions;
using FluentValidation;
using FluentValidation.Results;
using Core.Common.Contracts;
using StructureMap;
using System.ComponentModel.Composition.Hosting;

namespace Core.Common.Core {
    /// <summary>
    /// Base client applied to the Client Entities
    /// </summary>
    public abstract class ObjectBase : NotificationObject, IDirtyCapable, System.ComponentModel.IDataErrorInfo {

        public ObjectBase() {
            _Validator = GetValidator();
            Validate();
        }

        protected bool _IsDirty = false;
        protected IValidator _Validator = null;

        protected IEnumerable<ValidationFailure> _ValidationErrors = null;

        public static IContainer SMContainer { get; set; }
        public static CompositionContainer Container { get; set; }

        [NotNavigable]
        public bool IsDirty {
            get { return _IsDirty; }
        }

        public List<IDirtyCapable> GetDirtyObjects() {
            List<IDirtyCapable> dirtyObjects = new List<IDirtyCapable>();

            WalkObjectsGraph(o => {
                if (o.IsDirty) {
                    dirtyObjects.Add(o);
                }

                return false;
            }, col1 => { });

            return dirtyObjects;
        }

        public void CleanAll() {
            List<ObjectBase> dirtyObjects = new List<ObjectBase>();

            WalkObjectsGraph(o => {
                if (o._IsDirty) {
                    o._IsDirty = false;
                }

                return false;
            }, col1 => { });
        }

        public bool IsAnythingDirty() {
            bool isDirty = false;

            WalkObjectsGraph(o => {
                if (o.IsDirty) {
                    isDirty = true;
                    return true; // short circuit
                }
                else {
                    return false;
                }
            }, col1 => { });

            return isDirty;
        }

        protected void WalkObjectsGraph(Func<ObjectBase, bool> snippetForObject,
                                        Action<IList> snippetForCollection,
                                        params string[] exemptProperties) 
        {
            List<ObjectBase> visited = new List<ObjectBase>();
            Action<ObjectBase> walk = null;

            List<string> exemptions = new List<string>();
            if (exemptProperties != null)
                exemptions = exemptProperties.ToList();

            walk = (o) => {
                if (o != null && !visited.Contains(o)) {
                    visited.Add(o);

                    bool exitWalk = snippetForObject.Invoke(o);

                    if (!exitWalk) {
                        PropertyInfo[] properties = o.GetBrowsableProperties();

                        foreach (var property in properties) {
                            if (!exemptions.Contains(property.Name)) {
                                if (property.PropertyType.IsSubclassOf(typeof(ObjectBase))) {
                                    ObjectBase obj = (ObjectBase)(property.GetValue(o, null));
                                    walk(obj);
                                }
                                else {
                                    IList col1 = property.GetValue(o, null) as IList;
                                    if (col1 != null) {
                                        snippetForCollection.Invoke(col1);

                                        foreach (object item in col1) {
                                            if (item is ObjectBase)
                                                walk((ObjectBase)item);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            walk(this);
        }

        

        #region Property Change Notification

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            OnPropertyChanged(true, propertyName);
        }

        protected virtual void OnPropertyChanged(bool makeDirty, [CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (makeDirty)
                _IsDirty = true;

            Validate();
        }

        #endregion

        #region Validation

        protected virtual IValidator GetValidator() {
            return null;
        }

        [NotNavigable]
        public IEnumerable<ValidationFailure> ValidationErrors {
            get { return _ValidationErrors; }
            set { }
        }

        public void Validate() {
            if (_Validator != null) {
                ValidationResult results = _Validator.Validate(this);
                _ValidationErrors = results.Errors;
            }
        }

        [NotNavigable]
        public virtual bool IsValid {
            get{
                if(_ValidationErrors != null && _ValidationErrors.Count() > 0)
                    return false;
                else
                    return true;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error {
            get { return string.Empty; }
        }

        public string this[string columnName] {
            get {
                StringBuilder errors = new StringBuilder();

                if (_ValidationErrors != null && _ValidationErrors.Count() > 0) {
                    foreach (ValidationFailure validationError in _ValidationErrors) {
                        if (validationError.PropertyName == columnName)
                            errors.AppendLine(validationError.ErrorMessage);
                    }
                }

                return errors.ToString();
            }
        }

        #endregion
    }
}

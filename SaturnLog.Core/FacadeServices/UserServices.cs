using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
using SaturnLog.Core.Exceptions;

namespace SaturnLog.Core
{
    public class UserServices
    {
        #region Private Fields and Properties
        private App _app;
        #endregion

        #region Private Properties
        private LogsContentConstructor LogsContentConstructor { get { return this._app.LogsContentConstructor; } }

        private IDataRepository DataRepository { get { return this._app.DataRepository; } }
        private IUserRepository UserRepository { get { return this._app.UserRepository; } }
        private ISaturn5Repository Saturn5Repository { get { return this._app.Saturn5Repository; } }
        private ISaturns5DashboardRepository Saturns5DashboardRepository { get { return this._app.Saturns5DashboardRepository; } }
        #endregion

        #region Constructor
        public UserServices(App app)
        {
            this._app = app;
        }
        #endregion
        
        #region Methods
        // Crud - Create
        #region Create
        // Create new user.
        public async Task CreateAsync(string username, string firstName, string surname, UserType type)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                if (username is null) throw new ArgumentNullException("username is required parameter.", nameof(username));
                else if (firstName is null) throw new ArgumentNullException("firstName is required parameter.", nameof(firstName));
                else if (surname is null) throw new ArgumentNullException("surname is required parameter.", nameof(surname));
                else if (username.Trim() == "") throw new ArgumentException("username cannot be empty.", nameof(username));
                else if (firstName.Trim() == "") throw new ArgumentException("firstName cannot be empty.", nameof(firstName));
                else if (surname.Trim() == "") throw new ArgumentException("surname cannot be empty.", nameof(surname));

                // Assure that all the characters used in provided username string are upper case, and 'empty space' doesn't perpend, or append the username.
                username = username.Trim().ToUpper();
                firstName = firstName.Trim().ToUpperFirstCharOnly();
                surname = surname.Trim().ToUpperFirstCharOnly();

                // Throw appropriate exception if 
                if (this.UserRepository.HasUsername(username))
                    throw new ArgumentException($"Provided username: {username} is invalid because is already associated with existing user. Please provide unique username.", nameof(username));

                // Gather ..
                this.GatherAddData(username, firstName, surname, type, out string addUserLog);

                // Commit ..
                await Task.Run(() => { this.CommitAddData(username, firstName, surname, type, addUserLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }
        #endregion

        // Crud - Read
        #region Read
        // Answers the question whether provided string is recognized as valid user username.
        public bool HasUsername(string username)
        {
            // Parameters validation
            if (username is null) throw new ArgumentNullException(username);

            // Assure that all the characters used in provided username string are upper case, and 'empty space' doesn't perpend, or append the string.
            username = username.Trim().ToUpper();

            return this.UserRepository.HasUsername(username);
        }

        // Returns user associated with provided username.
        public User Get(string username)
        {
            // Validate parameters
            if (username is null) throw new ArgumentNullException("Username is required parameter.", nameof(username));

            // Assure that all the characters used in provided username string are upper case, and 'empty space' doesn't perpend, or append the username.
            username = username.Trim().ToUpper();

            if (!this.UserRepository.HasUsername(username))
                throw new ArgumentException($"Unable to obtain user associated with provided username: {username}. Provided username is not associated with any existing user.", nameof(username));

            return this.UserRepository.Read(username);
        }

        // Returns user associated with provided username asynchronously.
        public async Task<User> GetAsync(string username)
        {
            return await Task.Run<User>(() => { return this.Get(username); });
        }
        #endregion

        // Crud - Update
        #region Update
        public async Task EditAsync(string username, string newFirstName, string newSurname, UserType? newUserType)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Validate parameter
                if (username is null) throw new ArgumentNullException("Username is required parameter.", nameof(username));
                else if (newFirstName is null && newSurname is null && newUserType is null) throw new ArgumentNullException("Either newFirstName, newSurname or newUserType must not be null.");
                else if (newFirstName?.Trim() == "") throw new ArgumentException("newFirstName cannot be empty.", nameof(newFirstName));
                else if (newSurname?.Trim() == "") throw new ArgumentException("newSurname cannot be empty.", nameof(newSurname));

                // Assure that all the characters used in provided username string are upper case, and 'empty space' doesn't perpend, or append the username.
                username = username.Trim().ToUpper();
                newFirstName = newFirstName?.Trim()?.ToUpperFirstCharOnly();
                newSurname = newSurname?.Trim()?.ToUpperFirstCharOnly();

                // If provided username is not recognized throw exception
                if (!this.UserRepository.HasUsername(username))
                    throw new ArgumentException($"Unable to edit user associated with provided username: {username}. Provided username is not associated with any existing user.", nameof(username));
                // If provided username represents username of the currently logged in user throw exception
                else if (this._app.LoggedUsername == username)
                    throw new AttemptToEditLoggedInUserException(this._app.LoggedUser);

                // Gather .. 
                this.GatherEditData(username, ref newFirstName, ref newSurname, ref newUserType, out string editLog);

                // Adjust .. 
                // .. nothing,
                
                // Commit ..
                await Task.Run(() => { this.CommitEditData(username, newFirstName, newSurname, newUserType, editLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }


        }
        #endregion

        // Crud - Delete
        #region Delete
        // Removes user associated with specified username asynchronously
        public async Task RemoveAsync(string username)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Validate parameter
                if (username is null) throw new ArgumentNullException("Username is required parameter.", nameof(username));

                // Assure that all the characters used in provided username string are upper case, and 'empty space' doesn't perpend, or append the username.
                username = username.Trim().ToUpper();

                // Throw exception if provided username is not recognized
                if (!this.UserRepository.HasUsername(username))
                    throw new ArgumentException($"Unable to remove user associated with provided username: {username}. Provided username is not associated with any existing user.", nameof(username));
                // If provided username represents username of the currently logged in user throw exception
                else if (this._app.LoggedUsername == username)
                    throw new AttemptToRemoveLoggedInUserException(this._app.LoggedUser);

                // Gather ..
                this.GatherRemoveData(username, out string userRemoveLog);

                // Commit ..
                await Task.Run(() => { this.CommitRemoveData(username, userRemoveLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }

        }
        #endregion
        #endregion

        #region Private Fields
        #region Gather
        private void GatherAddData(string username, string firstName, string surname, UserType type, out string addUserLog)
        {
            // Get add new user log
            addUserLog = this.LogsContentConstructor.GetUserCreatedLog(username, firstName, surname, type);
        }

        private void GatherEditData(string username, ref string newFirstName, ref string newSurname, ref UserType? newUserType, out string editLog)
        {
            // Get instance of the user to be edited, and build the user edit log based on it.
            User toBeEdited = this.UserRepository.Read(username);

            // If string provided as new first name and current user first name are equal set it to null.
            if (toBeEdited.FirstName == newFirstName)
                newFirstName = null;

            // If string provided as new surname and current user surname are equal set it to null.
            if (toBeEdited.Surname == newSurname)
                newSurname = null;

            // If string provided as new UserType and current user UserType are equal set it to null.
            if (toBeEdited.Type == newUserType)
                newUserType = null;

            // Get appropriate user edit log depending on which user parameters getting edited.
            if (!(newFirstName is null) && !(newSurname is null) && !(newUserType is null))
                editLog = this.LogsContentConstructor.GetUserEditFirstNameSurnameAndUserTypeLog(toBeEdited, newFirstName, newSurname, newUserType.GetValueOrDefault());
            else if (!(newFirstName is null) && !(newSurname is null))
                editLog = this.LogsContentConstructor.GetUserEditFirstNameAndSurnameLog(toBeEdited, newFirstName, newSurname);
            else if (!(newFirstName is null) && !(newUserType is null))
                editLog = this.LogsContentConstructor.GetUserEditFirstNameAndUserTypeLog(toBeEdited, newFirstName, newUserType.GetValueOrDefault());
            else if (!(newFirstName is null) && !(newUserType is null))
                editLog = this.LogsContentConstructor.GetUserEditSurnameAndUserTypeLog(toBeEdited, newSurname, newUserType.GetValueOrDefault());
            else if (!(newFirstName is null))
                editLog = this.LogsContentConstructor.GetUserEditFirstNameLog(toBeEdited, newFirstName);
            else if (!(newSurname is null))
                editLog = this.LogsContentConstructor.GetUserEditSurnameLog(toBeEdited, newSurname);
            else if (!(newUserType is null)) 
                editLog = this.LogsContentConstructor.GetUserEditUserTypeLog(toBeEdited, newUserType.GetValueOrDefault());
            else
                throw new ArgumentNullException("Either newFirstName, newSurname or newUserType must not be null.");
        }

        private void GatherRemoveData(string username, out string userRemoveLog)
        {
            // Get instance of the user to be removed, and build the user removal log based on it.
            User toBeRemovedUser = this.UserRepository.Read(username);
            userRemoveLog = this.LogsContentConstructor.GetUserRemovedLog(toBeRemovedUser);
        }
        #endregion

        #region Commit
        private void CommitAddData(string username, string firstName, string surname, UserType type, string addUserLog)
        {
            // Persists new user in the repository
            this.UserRepository.Create(username, firstName, surname, type);

            // Log User creation in the currently logged in user personal log.
            this.UserRepository.AddUserLog(username, addUserLog);
        }

        private void CommitEditData(string username, string newFirstName, string newSurname, UserType? newType, string userEditLog)
        {
            // Update user data 
            this.UserRepository.Update(username, newFirstName, newSurname, newType);

            // Log Saturn edit into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, userEditLog);
            
            // Get edited instance of the user from the users repository.
            User editedUser = this.UserRepository.Read(username);

            // Commit changes into the Saturns5Dashboard
            this.Saturns5DashboardRepository.UpdateUserDetails(editedUser);
        }

        private void CommitRemoveData(string username, string userRemoveLog)
        {
            // Remove saturn5 from the repository
            this.UserRepository.Delete(username);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, userRemoveLog);
        }
        #endregion
        #endregion
    }
}

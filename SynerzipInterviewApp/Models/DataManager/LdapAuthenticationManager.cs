using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SynerzipInterviewApp.Models.Repository;

namespace SynerzipInterviewApp.Models.DataManager
{
    public class LdapAuthenticationManager : IAuthenticationRepository
    {
        private readonly LdapConfig config;

        PrincipalContext ctx;

        public LdapAuthenticationManager(IOptions<LdapConfig> config)
        {
            this.config = config.Value;

        }

        public User Login(Login login)
        {
            try
            {
                // Additional check to User in perticular group.
                //bool isGroupMember = false;
                //using (PrincipalContext context = new PrincipalContext(ContextType.Domain, config.UserDomainName))
                //{
                //    if (context != null)
                //    {
                //        //UserPrincipal user = UserPrincipal.Current
                //        using ( UserPrincipal user = UserPrincipal.FindByIdentity(context, login.UserName))
                //        {
                //            if (user != null)
                //            {
                //                using (GroupPrincipal group = GroupPrincipal.FindByIdentity(context, config.GroupName))
                //                {
                //                    if (group != null)
                //                    {
                //                        isGroupMember = true; //user.IsMemberOf(group);
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                //return ValidateUser(login, true);
                User UserObj = new User();
                UserObj.Email = "testuser@synerzip.com";
                UserObj.DisplayName = login.UserName;
                UserObj.GivenName = "testuser";
                UserObj.Surname = string.Empty;
                //UserObj.Id = foundUsr.EmployeeId;
                UserObj.isAuthenticated = true;
                return UserObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private User ValidateUser(Login login, bool isGroupMember)
        {
            try
            {
                if (isGroupMember)
                {
                    using (ctx = new PrincipalContext(ContextType.Domain, config.UserDomainName, login.UserName, login.Password))
                    {
                        bool bValid = ctx.ValidateCredentials(login.UserName, login.Password, ContextOptions.Negotiate | ContextOptions.Signing | ContextOptions.Sealing);

                        // Additional check to search user in directory.
                        if (bValid)
                        {
                            UserPrincipal prUsr = new UserPrincipal(ctx);
                            prUsr.SamAccountName = login.UserName;
                            PrincipalSearcher srchUser = new PrincipalSearcher(prUsr);

                            using (UserPrincipal foundUsr = srchUser.FindOne() as UserPrincipal)
                            {
                                if (foundUsr != null)
                                {
                                    User UserObj = new User();
                                    UserObj.Email = foundUsr.EmailAddress;
                                    UserObj.DisplayName = foundUsr.DisplayName;
                                    UserObj.GivenName = foundUsr.GivenName;
                                    UserObj.Surname = foundUsr.Surname;
                                    //UserObj.Id = foundUsr.EmployeeId;
                                    UserObj.isAuthenticated = true;
                                    return UserObj;
                                }
                            }
                        }
                        return new User();
                    }
                }
                else
                {
                    return new User();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
       
        }

        public List<User> GetUsers()
        {
            try
            {
                List<User> allUsers = new List<User>();
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, config.UserDomainName))
                {
                    using (UserPrincipal userPrincipal = new UserPrincipal(context))
                    {
                        using (PrincipalSearcher search = new PrincipalSearcher(userPrincipal))
                        {
                            foreach (UserPrincipal result in search.FindAll())
                                if (!String.IsNullOrEmpty(result.EmployeeId))
                                {
                                    allUsers.Add(new User
                                    {
                                        DisplayName = result.DisplayName,
                                        Email = result.EmailAddress,
                                        GivenName = result.GivenName,
                                        Surname = result.Surname,
                                        //Id = Int32.Parse(result.EmployeeId),
                                    });
                                }
                            var userList = allUsers.OrderBy(s => s.GivenName);

                            return userList.ToList();
                        } 
                    }
                }
            } 
                
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

    }
}

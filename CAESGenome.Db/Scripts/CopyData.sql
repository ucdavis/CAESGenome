/*
  Wipe any existing data, so we don't get conflicting keys
*/

delete from Barcodes
delete from userjobplates
delete from userjobs
delete from UserProfilesXRechargeAccounts
delete from RechargeAccounts
delete from webpages_UsersInRoles
delete from userprofile
delete from webpages_Membership

/* copy user profiles */

-- set the username for those that are blank
update [user]
set username = firstname + '.' + lastname + '@fake.com'
where username is null


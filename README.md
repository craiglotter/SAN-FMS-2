# SAN FMS 2

Commerce IT runs Commerce SAN, a storage area network solution for the Commerce Faculty. SAN FMS 2 is a background application that runs on the server hosting the SAN and is responsible for the following tasks to occur 15 minutes:

- Remove any invalid staff and student entries from the SAN controller database
- Remove any orphaned account folders from the system
- Create new account folders for any new accounts registered in the SAN controller database
- Generate an updated FileZilla config XML document based on the updated database
- Force FileZilla to reload its config page without restarting the server.
- Notify New and Removed users of their status via email.

Created by Craig Lotter, January 2008

*********************************

Project Details:

Coded in Visual Basic .NET using Visual Studio .NET 2008
Implements concepts such as Timer Use, Database Access, Folder Manipulation, Webpage Interaction, Email.
Level of Complexity: Simple

*********************************

Update 20080201.02:

- Sends an notification email on program startup and program shutdown

*********************************

Update 20080223.03:

- Checks valid users aren't using more space than their allotted size limit. If yes, then their accounts are marked 'locked' and they are excluded from the FTP server's user list. A notification mail is also sent to them.

*********************************

Update 20080417.04:

- Added user account folder count (excludes folders with names starting with '_'
- Added user account file count
- Added user account total usage limit calculation
- Added user account current space usage count
- Sets 'last scan' variable in database

*********************************

Update 20080514.05:

- Bug Fix: Error in code prevented _vti folders from being excluded from the user stats generation routine
- Now removes Thumbs.db and Desktop.ini from user folders during user stats generation routine

*********************************

Update 20080615.06:

- Added function to generate A-TEMPXXX user (student) accounts
- Added function to remove A-TEMPXXX user (student) accounts
- Captures Account Created timestamp
- Now handles temporary accounts that don't have associated Novell network accounts

*********************************

Update 20080620.07:

- Mail server, System From Address, Report To Email Address are all now set in the config.sav file
- Daily Report time now set in config.sav file (format 00:30:00)
- Bug Fix: operation only executes if all four necessary variables are set to valid files/folders
- Bug Fix: fixed erroneous status label descriptions
- Bug Fix: error and activity log files are no longer locked by daily report mail send

*********************************

Update 20081003.08:

- Remove unused, empty SAN accounts after a month
- Remove temporary accounts after two weeks

select * from iosjournal where jid = 'NPM' and aid = 'NPM16157'

select * From iosjournaldetails where journalid = 'J20171100014'

select distinct articletype  From iosjournaldetails order by articletype

select * from iosjournaldetails where  A articletype='login_ios'

select * from login_ios

nsert into login_ios
 values
  ('sid','sid','admin')
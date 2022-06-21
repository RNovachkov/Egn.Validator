# Egn.Validator
##### Simple project for Bulgarian personal Identification number validation on C Sharp

This project is basicly traslated php project from [ Georgi Chorbadzhiyski](https://georgi.unixsol.org " Georgi Chorbadzhiyski"), for the original project [click here](https://georgi.unixsol.org/programs/egn.php "click here")
You can find the information, about how the validation is processed in [Wikipedia](https://bg.wikipedia.org/wiki/Единен_граждански_номер "Wikipedia") as well

------------

##### Basic usege:
- As method -> **bool isValid = Egn.isValid({the Personal id you want to validate in string});**
- As ValidationAttribute -> \
	  **[EGN]**\
	  **public string personalId { get; set; }**\
	or\
	  **[EGN(ErrorMessage="Your error message")]**\
	  **public string personalId { get; set; }**

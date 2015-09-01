To install:
	Extract DocSender.zip to your chosen folder (i.e. C:/Program Files/DocSender)
	Run file - install.bat

Starting service:	
	Open services in Computer management.
	In config file(EFx.exe.config) find node with key "serviceName". Find this in opened services.
	i.e. node "<add key="serviceName" value="Scanned docs to ftp sender"/>" look for "Scanned docs to ftp sender" service.
	Start service.


To uninstall:
	Stop service.
	Run uninstall.bat

--------- Settings (EFx.exe.config) -----------
	
Check interval:
	Default - 1h (if not specified)

Logging: 
	Default - 100MB (if not specified)
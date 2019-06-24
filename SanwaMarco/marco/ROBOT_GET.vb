Function RobotGet() AS String;

    SETVAR("@dest","ELPT1");'DEBUG:設定值測試
    '檢查參數,目的地不存在時, return NAK 
    RETURN("NAK:dest_empty","'@<dest>' = 'undefined'");
    '設定點位(方便閱讀，拆成5行)
	SETVAR("@pno",DECODE("@<dest>","ELPT1",1,"ELPT2",2,"ILPT1",3,"ILPT2",4,"@<pno>"));
	SETVAR("@pno",DECODE("@<dest>","SHELF11",11,"SHELF12",12,"SHELF13",13,"SHELF14",14,"SHELF15",15,"@<pno>"));
	SETVAR("@pno",DECODE("@<dest>","SHELF21",21,"SHELF22",22,"SHELF23",23,"SHELF24",24,"SHELF25",25,"@<pno>"));
	SETVAR("@pno",DECODE("@<dest>","SHELF31",31,"SHELF32",32,"SHELF33",33,"SHELF34",34,"@<pno>"));
	SETVAR("@pno",DECODE("@<dest>","SHELF41",41,"SHELF42",42,"SHELF43",43,"SHELF44",44,"@<pno>"));
	RETURN("NAK:pno_unknow,@<dest>","'@<pno>' = 'undefined'");

	'檢查來源地在席  Sensor
	SETVAR("@sensor",DECODE("@<dest>","ELPT1",101;102;103,"ELPT2",201;202;203,"ILPT1",301;302;303,"ILPT2",309;310;311,"@<sensor>"));
	SETVAR("@sensor",DECODE("@<dest>","SHELF11",500;501,"SHELF12",508;509,"SHELF13",600;601,"SHELF14",608;609,"SHELF15",1800;1801,"@<sensor>"));
	SETVAR("@sensor",DECODE("@<dest>","SHELF21",502;503,"SHELF22",510;511,"SHELF23",602;603,"SHELF24",610;611,"SHELF25",1802;1803,"@<sensor>"));
	SETVAR("@sensor",DECODE("@<dest>","SHELF31",504;505,"SHELF32",512;513,"SHELF33",604;605,"SHELF34",612;613,"@<sensor>"));
	SETVAR("@sensor",DECODE("@<dest>","SHELF41",506;507,"SHELF42",514;515,"SHELF43",606;607,"SHELF44",614;615,"@<sensor>"));
	RETURN("NAK:sensor_unknow,@<dest>","'@<sensor>' = 'undefined'");
	API("I7565DNM_CHECKIO_ON", true, "NAK:PRESENCE_ERROR,@<sensor>");'檢查
	'SETVAR("@I7565DNM_CHECKIO_ON_RESULT","false");'DEBUG:設定值測試
	'PRINT("@dest:@<dest>,@pno:@<pno>,@sensor:@<sensor>,@I7565DNM_CHECKIO_ON_RESULT:@<I7565DNM_CHECKIO_ON_RESULT>");'DEBUG:檢查值
	'RETURN("NAK:PRESENCE_ERROR,@<sensor>","'@<I7565DNM_CHECKIO_ON_RESULT>' = 'FALSE'");'DEBUG用
	
	'如果是ELPT檢查 Shutter 是否打開
	SETVAR("@sensor",DECODE("@<dest>","ELPT1",1000,"ELPT2",1002));'1000 A部① Shutter 開,1002 A部② Shutter 開
	IF("'@<sensor>' <> 'undefined'")
		API("I7565DNM_CHECKIO_ON", true, "NAK:SHUTTER_IS_CLOSE,@<sensor>");'檢查
		'SETVAR("@I7565DNM_CHECKIO_ON_RESULT","false");'DEBUG:設定值測試
		'PRINT("@dest:@<dest>,@pno:@<pno>,@sensor:@<sensor>,@I7565DNM_CHECKIO_ON_RESULT:@<I7565DNM_CHECKIO_ON_RESULT>");'DEBUG:檢查值
		'RETURN("NAK:SHUTTER_IS_CLOSE,@<dest>","'@<I7565DNM_CHECKIO_ON_RESULT>' = 'FALSE'");'DEBUG用
	ENDIF
	
	'設定參數,執行 "$1CMD:GET__:pno,1,1,0,0"; '$1CMD:GET__:pno,slot,arm,al,opt[CR] 	
	SETVARS("@controller","$1","@slot",1,"@arm",1,"@al",0,"@opt",0);	
    API("ATEL_ROBOT_GET", true, "ERR:99999999");
	PRINT("@dest:@<dest>,@pno:@<pno>,@sensor:@<sensor>,@controller:@<controller>,@slot:@<slot>,@arm:@<arm>,@al:@<al>,@opt:@<opt>");'DEBUG:檢查值
	RETURN("@<ATEL_ROBOT_GET_RESULT>");

End Function;
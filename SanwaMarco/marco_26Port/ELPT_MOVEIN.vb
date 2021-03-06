﻿Function ELPT_MOVEIN();
    SETVAR("@device", "DNM01");
    'CHECK READY
    SETVAR("@io", DECODE("@<arg1>", "P1", "2002", "P2", "2102", "@<io>"));
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "1");
    SETVAR("@values", "1");
    API("I7565DNM_CHECK_IOS", False);
    Return ("Port not Ready.", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    '26Port 形式不包含AREA SENSOR
    'CHECK A AREA SENSOR(Nornal on)
    'SETVAR("@io", "710");
	'SETVAR("@interval", "200");
	'SETVAR("@retry_count", "1");
    'SETVAR("@values", "1");
    'API("I7565DNM_CHECK_IOS", False);
    'Return ("A AREA SENSOR ON", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    'CHECK SHUTTER
    SETVAR("@io", DECODE("@<arg1>", "P1", "704", "P2", "706"));
	SETVARS("@interval", "200", "@retry_count", "1", "@values", "1");
    API("I7565DNM_CHECK_IOS", False);
    Return ("Shutter not open.", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    'CHECK SERVO
    SETVAR("@io", DECODE("@<arg1>", "P1", "2003", "P2", "2103", "@<io>"));
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "1");
    SETVAR("@values", "1");
    API("I7565DNM_CHECK_IOS", False);
    Return ("PLEASE SERVO ON", "@<I7565DNM_CHECK_IOS_RETURN> = 0");
    'SET START 0
    SETVAR("@io", DECODE("@<arg1>", "P1", "2011", "P2", "2111", "@<io>"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'SET POSITION
   'DELAY(200);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2012;2013", "P2", "2112;2113", "@<io>"));
    SETVAR("@value", "1;1");
    API("I7565DNM_SETIOS", True);
    'START & SERVO ON 
    DELAY(500);
    SETVAR("@io", DECODE("@<arg1>", "P1", "2011", "P2", "2111", "@<io>"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    'DELAY(200);
    'SETVAR("@io", DECODE("@<arg1>", "P1", "2009", "P2", "2109", "@<io>"));
    'SETVAR("@value", "1");
    'API("I7565DNM_SETIOS", True);
    '檢查做動後的 io Sensor
    SETVAR("@io", DECODE("@<arg1>", "P1", "108;109;110", "P2", "111;112;113", "@<io>"));
    '檢查實際IO
	SETVAR("@interval", "200");
	SETVAR("@retry_count", "100");
    SETVAR("@values", "001");
    '26Port 形式不包含AREA SENSOR 
    '新增緊急停止功能
    'SETVAR("@ems_input", "710");
    'SETVAR("@ems_values", "1");
    '關閉 A1-SERVO(OFF) A2-SERVO(OFF) A1 Shutter開(OFF) A1 Shutter關(OFF) A2 Shutter開(OFF) A2 Shutter關(OFF)    
    'SETVAR("@ems_output", "2009;2109;904;905;906;907");
    'SETVAR("@ems_enabled", "0;0;0;0;0;0");
    API("I7565DNM_CHECK_IOS", True);
End Function;

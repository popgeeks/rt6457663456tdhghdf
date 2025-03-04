
-- --------------------------------------------------------

--
-- Structure for view `vw_randomcardsbylevel2`
--
DROP TABLE IF EXISTS `vw_randomcardsbylevel2`;

CREATE ALGORITHM=UNDEFINED DEFINER=`u925182332_darkwind`@`localhost` SQL SECURITY DEFINER VIEW `vw_randomcardsbylevel2`  AS SELECT `cards`.`id` AS `ID`, rand() AS `rand()` FROM `cards` WHERE `cards`.`level` = 2 ORDER BY rand() ASC LIMIT 0, 10 ;


-- --------------------------------------------------------

--
-- Table structure for table `recentscores`
--

CREATE TABLE `recentscores` (
  `idRecentScores` int(11) NOT NULL,
  `GameID` int(11) NOT NULL,
  `GameType` varchar(5) DEFAULT NULL,
  `Player1` varchar(45) NOT NULL,
  `Player2` varchar(45) NOT NULL,
  `Player3` varchar(45) DEFAULT NULL,
  `Player4` varchar(45) DEFAULT NULL,
  `P1Score` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `P2Score` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Last_Modified_Date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `recentscores`
--

INSERT INTO `recentscores` (`idRecentScores`, `GameID`, `GameType`, `Player1`, `Player2`, `Player3`, `Player4`, `P1Score`, `P2Score`, `Last_Modified_Date`) VALUES
(1169425, 2816310, 'OTT', 'forumadmin', 'TestAccount2', NULL, NULL, 9, 13, '2020-02-17 19:38:09'),
(1169426, 2816348, 'OTT', 'Slithered', 'Camper44', NULL, NULL, 5, 5, '2020-02-27 02:53:57'),
(1169427, 2816349, 'OTT', 'mizuryuu83', 'Camper44', NULL, NULL, 5, 5, '2020-02-27 02:57:00'),
(1169428, 2816351, 'TOTT', 'Zeromus', '', '', '', 10, 10, '2020-08-11 04:00:01'),
(1169429, 2816482, 'OTT', 'Javierdekay1', 'Bocadillo8eu', NULL, NULL, 14, 8, '2021-01-27 02:23:19'),
(1169430, 2816485, 'TTW', 'Javierdekay1', 'Bocadillo8eu', NULL, NULL, 858, 918, '2021-01-27 02:39:25'),
(1169431, 2816563, 'OTT', 'matparkes', 'Dark Zeurra', NULL, NULL, 11, 16, '2021-05-05 00:31:00'),
(1169432, 2816564, 'TTW', 'matparkes', 'Dark Zeurra', NULL, NULL, 547, 0, '2021-05-05 00:57:47'),
(1169433, 2816502, 'TTW', 'All110', 'DannyMolotov', NULL, NULL, 1000, 1000, '2021-05-11 15:31:24'),
(1169434, 2816706, 'OTT', 'Ginshuto', 'LucasBlight ', NULL, NULL, 16, 12, '2021-09-01 22:20:03'),
(1169435, 2816709, 'TTW', 'Ginshuto', 'LucasBlight', NULL, NULL, 0, 438, '2021-09-02 19:04:03'),
(1169436, 2816834, 'OTT', 'ScubaStan', 'hendrix', NULL, NULL, 9, 10, '2022-03-04 06:11:45'),
(1169437, 2816835, 'OTT', 'hendrix', 'ScubaStan', NULL, NULL, 5, 5, '2022-03-04 06:13:10'),
(1169439, 2816882, 'TTW', 'Cloud7777777', 'SquallShot84', NULL, NULL, 940, 800, '2022-03-22 20:17:25'),
(1169440, 2816961, 'TTW', 'aza06', 'Azazel06', NULL, NULL, 1000, 838, '2022-05-20 21:04:32'),
(1169441, 2816959, 'OTT', 'aza06', 'Azazel06', NULL, NULL, 5, 6, '2022-05-20 20:58:03'),
(1169442, 2816960, 'OTT', 'aza06', 'Azazel06', NULL, NULL, 12, 14, '2022-05-20 21:02:07'),
(1169445, 2817016, 'OTT', 'Brezedela', 'Qilinleon', NULL, NULL, 5, 5, '2022-08-17 02:18:28'),
(1169446, 2817017, 'OTT', 'Qilinleon', 'Brezedela', NULL, NULL, 5, 5, '2022-08-17 02:20:19'),
(1169448, 2817026, 'OTT', 'PsychoJoona', 'ozku02', NULL, NULL, 5, 5, '2022-08-30 06:49:05'),
(1169449, 2817054, 'OTT', 'Ponyboyxiii', 'Slithered', NULL, NULL, 5, 5, '2023-04-27 11:03:48'),
(1169450, 2817055, 'OTT', 'Ponyboyxiii', 'Slithered', NULL, NULL, 5, 5, '2023-04-27 11:07:43'),
(1169451, 2817056, 'OTT', 'Slithered', 'Ponyboyxiii', NULL, NULL, 5, 5, '2023-04-27 11:10:18');

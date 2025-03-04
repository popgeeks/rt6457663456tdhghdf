
-- --------------------------------------------------------

--
-- Table structure for table `guildbanks_lu`
--

CREATE TABLE `guildbanks_lu` (
  `ID` int(10) UNSIGNED NOT NULL,
  `Player` varchar(45) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `guildbanks_lu`
--

INSERT INTO `guildbanks_lu` (`ID`, `Player`) VALUES
(1, 'BebopBank'),
(2, 'Catchbank'),
(3, 'DWBank'),
(4, 'NERVbank'),
(5, 'ultimaBANK'),
(6, 'TheTurksPA'),
(7, 'NERVSaver'),
(8, 'NintendoBank'),
(9, 'XIIIBANK'),
(10, 'JoHBank'),
(11, 'JoHSaver'),
(12, 'EdenXBank');

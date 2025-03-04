
-- --------------------------------------------------------

--
-- Table structure for table `xxx_tmp_cardpacks`
--

CREATE TABLE `xxx_tmp_cardpacks` (
  `cardname` varchar(250) NOT NULL,
  `player` varchar(45) NOT NULL,
  `uniquecard` tinyint(1) NOT NULL,
  `discovery` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

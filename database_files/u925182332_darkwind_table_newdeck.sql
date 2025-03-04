
-- --------------------------------------------------------

--
-- Table structure for table `newdeck`
--

CREATE TABLE `newdeck` (
  `ID` int(11) NOT NULL,
  `DeckKeyword` varchar(150) NOT NULL,
  `DeckDescription` varchar(150) DEFAULT NULL,
  `DeckMetaKeywords` varchar(45) DEFAULT NULL,
  `DeckAuthor` varchar(100) DEFAULT NULL,
  `CreatedDate` varchar(45) DEFAULT NULL,
  `IsSpecial` tinyint(3) DEFAULT NULL,
  `StartDate` varchar(45) DEFAULT NULL,
  `ExpireDate` varchar(45) DEFAULT NULL,
  `IsApproved` tinyint(3) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `newdeck`
--

INSERT INTO `newdeck` (`ID`, `DeckKeyword`, `DeckDescription`, `DeckMetaKeywords`, `DeckAuthor`, `CreatedDate`, `IsSpecial`, `StartDate`, `ExpireDate`, `IsApproved`) VALUES
(1, 'DragonWarrior', 'Dragon Warrior', NULL, 'forumadmin', '2021-08-06 02:23:35', 0, NULL, NULL, 1);

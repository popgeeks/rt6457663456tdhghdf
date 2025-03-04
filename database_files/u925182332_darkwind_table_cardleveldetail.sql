
-- --------------------------------------------------------

--
-- Table structure for table `cardleveldetail`
--

CREATE TABLE `cardleveldetail` (
  `CardLevelDetailID` int(11) NOT NULL,
  `CardLevel` int(11) NOT NULL,
  `CardLevelDescription` varchar(45) NOT NULL,
  `CardLevelClass` varchar(45) NOT NULL,
  `CardLevelFlgGold` int(11) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `cardleveldetail`
--

INSERT INTO `cardleveldetail` (`CardLevelDetailID`, `CardLevel`, `CardLevelDescription`, `CardLevelClass`, `CardLevelFlgGold`) VALUES
(1, 1, 'Common', 'cvCommon', 0),
(2, 2, 'Common', 'cvCommon', 0),
(3, 3, 'Common', 'cvCommon', 0),
(4, 4, 'Common', 'cvCommon', 0),
(5, 5, 'Common', 'cvCommon', 0),
(6, 6, 'Uncommon', 'cvUncommon', 0),
(7, 7, 'Uncommon', 'cvUncommon', 0),
(8, 8, 'Rare', 'cvRare', 0),
(9, 9, 'Rare', 'cvRare', 0),
(10, 10, 'Epic', 'cvEpic', 0),
(11, 10, 'Legendary', 'cvLegendary', 1),
(12, 11, 'Mythical', 'cvMythical', 1);

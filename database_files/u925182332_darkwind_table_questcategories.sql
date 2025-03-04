
-- --------------------------------------------------------

--
-- Table structure for table `questcategories`
--

CREATE TABLE `questcategories` (
  `QuestCategoryID` int(10) UNSIGNED NOT NULL,
  `QuestCategoryDescription` varchar(300) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `questcategories`
--

INSERT INTO `questcategories` (`QuestCategoryID`, `QuestCategoryDescription`) VALUES
(1, 'Dailys'),
(2, 'Weeklys'),
(3, 'Monthlys'),
(4, 'One Time');


-- --------------------------------------------------------

--
-- Table structure for table `questrewardcategory`
--

CREATE TABLE `questrewardcategory` (
  `QuestRewardCategoryID` int(10) UNSIGNED NOT NULL,
  `QuestRewardCategoryDescription` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `questrewardcategory`
--

INSERT INTO `questrewardcategory` (`QuestRewardCategoryID`, `QuestRewardCategoryDescription`) VALUES
(1, 'For AP'),
(2, 'For Gold'),
(3, 'For Membership'),
(4, 'For Tokens'),
(5, 'For Cash'),
(6, 'For Achievements');

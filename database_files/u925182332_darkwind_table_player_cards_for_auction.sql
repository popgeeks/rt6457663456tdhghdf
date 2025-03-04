
-- --------------------------------------------------------

--
-- Table structure for table `player_cards_for_auction`
--

CREATE TABLE `player_cards_for_auction` (
  `id` int(10) NOT NULL,
  `seller` varchar(45) DEFAULT NULL,
  `sellDate` datetime DEFAULT NULL,
  `buyer` varchar(45) DEFAULT NULL,
  `buyDate` datetime DEFAULT NULL,
  `cardID` int(10) DEFAULT NULL,
  `itemPriceGold` int(10) DEFAULT NULL,
  `itemPriceCrystals` int(10) DEFAULT NULL,
  `itemType` varchar(15) NOT NULL DEFAULT 'Card'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `player_cards_for_auction`
--

INSERT INTO `player_cards_for_auction` (`id`, `seller`, `sellDate`, `buyer`, `buyDate`, `cardID`, `itemPriceGold`, `itemPriceCrystals`, `itemType`) VALUES
(6, 'sahil', '2015-05-29 12:34:27', 'sahil', '2015-05-29 12:37:02', 87, 0, 75, 'Card'),
(7, 'sahil', '2015-05-29 12:34:34', NULL, NULL, 100, 0, 300, 'Card'),
(8, 'sahil', '2015-05-29 12:39:26', 'forumadmin', '2017-09-03 19:52:24', 313, 0, 150, 'Card'),
(9, 'forumadmin', '2017-09-03 19:47:48', 'forumadmin', '2017-09-03 19:52:02', 524, 20, 300, 'Card'),
(10, 'forumadmin', '2017-09-03 19:57:14', 'aza06', '2022-05-20 16:53:54', 766, 10, 150, 'Card'),
(11, 'forumadmin', '2017-09-03 19:57:24', 'aza06', '2022-05-20 16:53:58', 4498, 5, 75, 'Card');

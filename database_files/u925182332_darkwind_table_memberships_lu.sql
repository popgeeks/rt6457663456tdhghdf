
-- --------------------------------------------------------

--
-- Table structure for table `memberships_lu`
--

CREATE TABLE `memberships_lu` (
  `ID` int(10) UNSIGNED NOT NULL,
  `Description` varchar(15) NOT NULL,
  `Tokens` int(10) UNSIGNED NOT NULL DEFAULT 10,
  `Gold` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `AP` float NOT NULL DEFAULT 0.1,
  `MembershipColor` varchar(15) NOT NULL,
  `SubscribeBonus` float NOT NULL DEFAULT 0.1,
  `ShopLimits` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `ShopTax` decimal(10,2) NOT NULL DEFAULT 0.00,
  `ExperienceBonus` decimal(10,2) NOT NULL DEFAULT 0.00,
  `GoldBonus` decimal(10,2) NOT NULL DEFAULT 0.00,
  `ForumGroupID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `MembershipClass` varchar(45) DEFAULT 'lvlNormal'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `memberships_lu`
--

INSERT INTO `memberships_lu` (`ID`, `Description`, `Tokens`, `Gold`, `AP`, `MembershipColor`, `SubscribeBonus`, `ShopLimits`, `ShopTax`, `ExperienceBonus`, `GoldBonus`, `ForumGroupID`, `MembershipClass`) VALUES
(1, 'Gold', 1, 2500, 2500, 'gold', 0.1, 25, 0.08, 0.05, 0.05, 9, 'lvlGold'),
(2, 'Platinum', 2, 5000, 5000, 'silver', 0.1, 30, 0.06, 0.10, 0.10, 10, 'lvlPlatinum'),
(3, 'Diamond', 4, 7500, 7500, 'blue', 0.1, 40, 0.03, 0.25, 0.30, 11, 'lvlDiamond'),
(4, 'Power User', 5, 10000, 10000, 'red', 0.1, 75, 0.00, 0.50, 0.70, 12, 'lvlPowerUser'),
(5, 'Normal', 0, 0, 0, 'green', 0, 15, 0.10, 0.00, 0.00, 3, 'lvlNormal'),
(6, 'VIP1', 0, 0, 0, 'darkblue', 0, 90, 0.00, 0.55, 0.75, 13, 'lvlVIP1'),
(7, 'VIP2', 0, 0, 0, 'darkblue', 0, 100, 0.00, 0.60, 0.80, 14, 'lvlVIP2'),
(8, 'VIP3', 0, 0, 0, 'darkblue', 0, 125, 0.00, 0.80, 1.00, 15, 'lvlVIP3'),
(9, 'VIP4', 0, 0, 0, 'darkblue', 0, 150, 0.00, 1.00, 1.25, 16, 'lvlVIP4');

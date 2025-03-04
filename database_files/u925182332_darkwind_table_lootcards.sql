
-- --------------------------------------------------------

--
-- Table structure for table `lootcards`
--

CREATE TABLE `lootcards` (
  `ID` int(10) UNSIGNED NOT NULL,
  `CardName` varchar(45) NOT NULL,
  `Award` varchar(100) NOT NULL,
  `Duration` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Lore` varchar(200) NOT NULL,
  `Consume` int(10) UNSIGNED NOT NULL DEFAULT 1,
  `Sellable` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `CashPrice` decimal(10,2) NOT NULL DEFAULT 0.00,
  `GoldPrice` varchar(45) NOT NULL DEFAULT '0',
  `Failure` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `FailureRate` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Active` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `lootcards`
--

INSERT INTO `lootcards` (`ID`, `CardName`, `Award`, `Duration`, `Lore`, `Consume`, `Sellable`, `CashPrice`, `GoldPrice`, `Failure`, `FailureRate`, `Active`) VALUES
(1, 'Essence of the Chocobo', 'GMEM', 43200, 'The essence of the chocobo grants you one (1) gold membership for a period of 30 days.  This card will be consumed upon redemption.', 1, 1, 5.00, '0', 0, 0, 1),
(2, 'Essence of the Moogle', 'PMEM', 43200, 'The essence of the moogle grants you one (1) platinum membership for a period of 30 days.  This card will be consumed upon redemption.', 1, 1, 10.00, '0', 0, 0, 1),
(3, 'Essence of the Dragon', 'DMEM', 43200, 'The essence of the dragon grants you one (1) diamond membership for a period of 30 days.  This card will be consumed upon redemption.', 1, 1, 15.00, '0', 0, 0, 1),
(4, 'Essence of the Phoenix', 'PUMEM', 43200, 'The essence of the phoenix grants you one (1) power user membership for a period of 30 days.  This card will be consumed upon redemption.', 1, 1, 20.00, '0', 0, 0, 1),
(5, 'Pandoras Redemption', 'TOKEN', 0, 'The power of Pandora\'s Redemption grants you 250 crystals.  This card will be consumed upon redemption.', 1, 1, 3.00, '0', 0, 0, 1),
(6, 'Scroll of Lesser Bounty', 'SHOP5', 43200, 'This scroll grants you an extra five (5) slots in your merchant. This card will be consumed upon redemption and is good for 30 days.', 1, 1, 1.00, '750', 0, 0, 0),
(7, 'Scroll of Minor Bounty', 'SHOP15', 43200, 'This scroll grants you an extra fifteen (15) slots in your merchant. This card will be consumed upon redemption and is good for 30 days.', 1, 1, 2.00, '1500', 0, 0, 0),
(8, 'Scroll of Superior Bounty', 'SHOP25', 43200, 'This scroll grants you an extra twenty-five (25) slots in your merchant. This card will be consumed upon redemption and is good for 30 days.', 1, 1, 3.00, '2250', 0, 0, 0),
(9, 'Scroll of Major Bounty', 'SHOP50', 43200, 'This scroll grants you an extra fifty (50) slots in your merchant. This card will be consumed upon redemption and is good for 30 days.', 1, 1, 4.25, '3000', 0, 0, 0),
(10, 'Scroll of Ancient Bounty', 'SHOP100', 43200, 'This scroll grants you an extra one hundred (100) slots in your merchant. This card will be consumed upon redemption and is good for 30 days.', 1, 1, 6.00, '5000', 0, 0, 0),
(11, 'Temper of Lesser Expansion', 'VAULT10', 0, 'This temper reinforces your vault to hold an additional ten (10) slots. This card will be consumed upon redemption.', 1, 1, 1.00, '1200', 0, 0, 1),
(12, 'Temper of Minor Expansion', 'VAULT25', 0, 'This temper reinforces your vault to hold an additional twenty-five (25) slots. This card will be consumed upon redemption.', 1, 1, 2.00, '2500', 0, 0, 1),
(13, 'Temper of Superior Expansion', 'VAULT50', 0, 'This temper reinforces your vault to hold an additional fifty (50) slots. This card will be consumed upon redemption.', 1, 1, 3.50, '4500', 0, 0, 1),
(14, 'Temper of Major Expansion', 'VAULT100', 0, 'This temper reinforces your vault to hold an additional one-hundred (100) slots. This card will be consumed upon redemption.', 1, 1, 5.00, '8000', 0, 0, 1),
(15, 'Temper of Ancient Expansion', 'VAULT250', 0, 'This temper reinforces your vault to hold an additional two-hundred and fifty (250) slots. This card will be consumed upon redemption.', 1, 1, 10.00, '15000', 0, 0, 1),
(16, 'Gnomish Toolset', 'VAULT5', 0, 'This device allows you to tweak your vault to hold an additional five (5) slots. This device may cause your attempt to fail. This card will be consumed upon redemption.', 1, 1, 1.00, '1000', 1, 50, 1),
(17, 'Devils Rain', 'RUSH5', 30, 'This magic card causes you to go into overdrive for 30 minutes causing your rush gauge to increase faster by five (5) percent.  This card will be consumed upon redemption.', 1, 1, 1.00, '500', 0, 0, 0),
(18, 'Circle of Fire', 'RUSH10', 30, 'Feeling trapped by the flames, you go into overdrive for 30 minutes thus causing your rush gauge to increase faster by ten (10) percent.  This card will be consumed upon redemption.', 1, 1, 1.00, '1000', 0, 0, 0),
(19, 'Rush Elixir', 'RUSH5', 60, 'This elixir causes you to go into ovedrive for 60 minutes.  The elixir may poison you instead and thus fail. This card will be consumed upon redemption.', 1, 1, 1.00, '500', 1, 30, 0),
(20, 'Force Field of Mental Preservation', 'RUSHBLOCK', 1440, 'This force field blocks Rush Gauge Decay for 24 hours.  The force field has a chance to fizzle and thus can fail. This card will be consumed upon redemption.', 1, 1, 1.00, '1000', 1, 30, 0),
(21, 'Force Field of Lesser Mental Preservation', 'RUSHBLOCK', 720, 'This force field blocks Rush Gauge Decay for 12 hours. This force field has a higher chance to fizzle and thus can fail.  This card will be consumed upon redemption.', 1, 1, 1.00, '500', 1, 60, 0),
(22, 'Elixir of Good Fortune', 'GP5', 300, 'This elixir will provide you with great fortune (5% gold) for up to five (5) hours. This card will be consumed upon redemption.', 1, 1, 1.00, '1000', 0, 0, 1),
(23, 'Elixir of Heightened Enlightenment', 'EXP5', 300, 'This elixir will provide you with a sense of enlightenment that allows you to earn five (5) percent faster.  The effect will wear off after five (5) hours. This card will be consumed upon redemption.', 1, 1, 1.00, '1000', 0, 0, 1),
(25, 'Temper of Lesser Guild Expansion', 'GVAULT10', 0, 'This temper reinforces your guild card vault to hold an additional ten (10) slots. This card will be consumed upon redemption.', 1, 1, 1.00, '1200', 0, 0, 1),
(26, 'Temper of Minor Guild Expansion', 'GVAULT25', 0, 'This temper reinforces your guild card vault to hold an additional twenty-five (25) slots. This card will be consumed upon redemption.', 1, 1, 1.80, '2500', 0, 0, 1),
(27, 'Temper of Superior Guild Expansion', 'GVAULT50', 0, 'This temper reinforces your guild card vault to hold an additional fifty (50) slots. This card will be consumed upon redemption.', 1, 1, 3.50, '4500', 0, 0, 1),
(28, 'Temper of Major Guild Expansion', 'GVAULT100', 0, 'This temper reinforces your guild card vault to hold an additional one-hundred (100) slots. This card will be consumed upon redemption.', 1, 1, 5.00, '8000', 0, 0, 1),
(29, 'Temper of Ancient Guild Expansion', 'GVAULT250', 0, 'This temper reinforces your guild card vault to hold an additional two-hundred and fifty (250) slots. This card will be consumed upon redemption.', 1, 1, 10.00, '17500', 0, 0, 1);

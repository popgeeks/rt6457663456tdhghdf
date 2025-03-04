
-- --------------------------------------------------------

--
-- Table structure for table `cashredeem`
--

CREATE TABLE `cashredeem` (
  `CashRedeemID` int(10) UNSIGNED NOT NULL,
  `CashRedeemDescription` varchar(45) NOT NULL,
  `CashRedeemConversion` int(10) UNSIGNED NOT NULL,
  `CashRedeemActiveStatus` int(10) UNSIGNED NOT NULL,
  `CashRedeemConversionInfo` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `cashredeem`
--

INSERT INTO `cashredeem` (`CashRedeemID`, `CashRedeemDescription`, `CashRedeemConversion`, `CashRedeemActiveStatus`, `CashRedeemConversionInfo`) VALUES
(1, 'Convert All Cash to Gold', 500, 1, '500 Gold for every $1.00'),
(2, 'Convert All Cash to Active Points', 1250, 1, '1250 AP for every $1.00'),
(3, 'Gold Membership', 500, 1, '1 Gold Membership to $5.00'),
(4, 'Platinum Membership', 1000, 1, '1 Platinum Membership to $10.00'),
(5, 'Diamond Membership', 1500, 1, '1 Diamond Membership to $15.00'),
(6, 'Power User Membership', 2000, 1, '1 Power User Membership to $20.00');

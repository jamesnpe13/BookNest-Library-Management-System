﻿
public enum AccountFilterKey
{
    ALL,
    ID,
    FIRST_NAME,
    LAST_NAME,
    USERNAME,
    EMAIL,
    ACCOUNT_TYPE
}

public enum BookFilterKey
{
    ALL,
    ID,
    ISBN,
    TITLE,
    GENRE,
    AUTHOR,
    YEAR_OF_PUBLICATION,
    PUBLISHER,
    STATUS,
    LIKES,
    SEARCH
}

public enum LoanTransactionFilterKey
{
    ALL,
    TRANSACTION_ID,
    ACCOUNT_ID,
    BOOK_ID,
    STATUS
}

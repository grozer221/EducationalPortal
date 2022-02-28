import {gql} from '@apollo/client';

export const USER_FRAGMENT = gql`
    fragment UserFragment on UserType {
        id
        firstName
        lastName
        middleName
        login
        email
        phoneNumber
        dateOfBirth
        role
        createdAt
        updatedAt
    }
`;

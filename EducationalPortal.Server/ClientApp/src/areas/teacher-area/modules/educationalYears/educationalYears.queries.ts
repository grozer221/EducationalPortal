import {gql} from '@apollo/client';
import {EDUCATIONAL_YEAR_FRAGMENT} from './educationalYears.fragments';
import {EducationalYear} from './educationalYears.types';

export type GetEducationalYearsData = { getEducationalYears: getEducationalYearsType }
export type getEducationalYearsType = { entities: EducationalYear[], total: number }

export type GetEducationalYearsVars = { page: number }

export const GET_EDUCATIONAL_YEARS_QUERY = gql`
    ${EDUCATIONAL_YEAR_FRAGMENT}
    query GetEducationalYears($page: Int!) {
        getEducationalYears(page: $page) {
            entities {
                ...EducationalYearFragment
            }
            total
        }
    }
`;

export type GetEducationalYearData = { getEducationalYear: EducationalYear }
export type GetEducationalYearVars = { id: string }

export const GET_EDUCATIONAL_YEAR_QUERY = gql`
    ${EDUCATIONAL_YEAR_FRAGMENT}
    query GetEducationalYear($id: ID!) {
        getEducationalYear(id: $id) {
            ...EducationalYearFragment
        }
    }
`;

import {gql} from '@apollo/client';
import {HOMEWORK_FRAGMENT} from './homeworks.fragments';
import {Homework, HomeworkStatus} from './homework.types';
import {Order} from '../../enums/order';

export type GetHomeworkData = { getHomework: Homework }
export type GetHomeworkVars = { id: string }

export const GET_HOMEWORK_QUERY = gql`
    ${HOMEWORK_FRAGMENT}
    query GetHomework($id: ID!) {
        getHomework(id: $id) {
            ...HomeworkFragment
        }
    }
`;


export type GetHomeworksData = { getHomeworks: getHomeworksType }
export type getHomeworksType = { entities: Homework[], total: number, pageSize: number }

export type GetHomeworksVars = { page: number, statuses: HomeworkStatus[] | null | undefined, order: Order}

export const GET_HOMEWORKS_QUERY = gql`
    ${HOMEWORK_FRAGMENT}
    query GetHomeworks($page: Int!, $statuses: [HomeworkStatus], $order: Order!) {
        getHomeworks(page: $page, statuses: $statuses, order: $order) {
            entities {
                ...HomeworkFragment
            }
            total
            pageSize
        }
    }
`;

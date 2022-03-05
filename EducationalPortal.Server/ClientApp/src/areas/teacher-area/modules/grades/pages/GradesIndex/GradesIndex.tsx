import React, {useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {message, Space, Table} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link} from 'react-router-dom';
import {GET_GRADES_QUERY, GetGradesData, GetGradesVars} from '../../grades.queries';
import {Grade} from '../../grades.types';
import {REMOVE_GRADE_MUTATION, RemoveGradeData, RemoveGradeVars} from '../../grades.mutations';

export const GradesIndex = () => {
    const [page, setPage] = useState(1);
    const getGradesQuery = useQuery<GetGradesData, GetGradesVars>(GET_GRADES_QUERY,
        {variables: {page: page}, fetchPolicy: 'network-only'},
    );
    const [removeGradeMutation, removeGradeMutationOptions] = useMutation<RemoveGradeData, RemoveGradeVars>(REMOVE_GRADE_MUTATION);

    const onRemove = (gradeId: string) => {
        removeGradeMutation({variables: {id: gradeId}})
            .then(async (response) => {
                await getGradesQuery.refetch({page});
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const columns: ColumnsType<Grade> = [
        {
            title: 'Назва',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text, grade) => (
                <ButtonsVUR viewUrl={`${grade?.id}`} updateUrl={`update/${grade?.id}`}
                            onRemove={() => onRemove(grade?.id)}/>
            ),
        },
    ];

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Link to={'create'}>
                <ButtonCreate/>
            </Link>
            <Table
                rowKey={'id'}
                loading={getGradesQuery.loading || removeGradeMutationOptions.loading}
                dataSource={getGradesQuery.data?.getGrades.entities}
                columns={columns}
                pagination={{
                    total: getGradesQuery.data?.getGrades.total,
                    onChange: async (newPage: number) => {
                        setPage(newPage);
                        await getGradesQuery.refetch({page: newPage});
                    },
                }}
            />
        </Space>
    );
};
